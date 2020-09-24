// import zipcode from '../../fixtures/zipCode.json'
// import zipCodes from '../fixtures/zipCode.json'
import zipcode from '../fixtures/zipCode.json'

// clusterList = {};

var zip = "65895";

describe('sample', function () {
    it('sample', async function () {
        var count = 0;
        var clusterList = [];
        // var zip = "";

        cy.visit('http://www.fedex.com/grd/maps/MapEntry.do');
        GetZipCode();
        console.log(zip);   
    
        cy.get('input[name="originZip"]').type("10038");
        cy.get('select[name="resType"]').select('YES');
        cy.get('input[value="View overnight map"]').click();
        cy.wait(5000);
       // console.log(zip); 
        // cy.get('table>tbody>tr>td.small').each(($data, index, $list) => {
           //    console.log($data, index, $list);
        //     //clusterList.push($data.text());
        //     //if (index == ($list.length - 1))
        //     //    ProcessZipCluster(zipcode.zipCodes[i], clusterList);
       //  });
       // zip = "10310";


        while (zip) {
            console.log(zip);   
            clusterList = [];
            console.log(clusterList);
            cy.get('input[name="originZip"]').clear();
            cy.get('input[name="originZip"]').type(zip);
            cy.get('input[value = "Update"]').click();
            cy.wait(5000);
            cy.get('table>tbody>tr>td.small').then(function (data) {
                clusterList = data.text().match(new RegExp('.{1,' + 13 + '}', 'g'));
                ProcessZipCluster(zip, clusterList);
            });
           await GetZipCode();
        }   
    })
})

function ProcessZipCluster(zipcode, data) {
   var ZipList = [];
    data.forEach(cluster => {
        let zips = cluster.split("-");
        ZipList.push({ Zipcode: zipcode, StartZip: zips[0].trim(), EndZip: zips[1].trim() })
    });
    PostToDB(ZipList);
}

function GetZipCode() {
    console.log(zip);
    return new Promise((resolve, reject) => {
        cy
            .request('GET', 'http://localhost:60957/Home/GetZipcode')
            .then(
                (response) => {
                    console.log(zip);
                    console.log(response.body);   
                    zip = response.body;
                    resolve();
                    
                }
                , err => {
                    console.log(err);
                    reject();
                }
            )
    });
}

function PostToDB(_data) {
    cy
        .request('POST', 'http://localhost:60957/Home/ImportOverNightMapData', _data )
        .then((response) => { 
            console.log(response);    
        })
}