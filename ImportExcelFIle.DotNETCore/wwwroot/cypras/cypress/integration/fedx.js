// import zipcode from '../../fixtures/zipCode.json'
// import zipCodes from '../fixtures/zipCode.json'
import zipcode from '../fixtures/zipCode.json'

// clusterList = {};

var ZipList = [];

describe('sample', function () {
    it('sample', function () {
        var count = 0;
        var zip = "";
        cy.visit('http://www.fedex.com/grd/maps/MapEntry.do');
        GetZipCode();
      
       // for (let i = 0; i < zipcode.zipCodes.length; i++) {

    
            var clusterList = [];

                cy.get('input[name="originZip"]').type("55555");
                cy.get('select[name="resType"]').select('YES');
                cy.get('input[value="View overnight map"]').click();
                cy.wait(5000);
                cy.get('table>tbody>tr>td.small').each(($data, index, $list) => {
                    // console.log($data, index, $list);
                    //clusterList.push($data.text());
                    //if (index == ($list.length - 1))
                    //    ProcessZipCluster(zipcode.zipCodes[i], clusterList);
                });
   

        while (zip) {
            clusterList = [];
            console.log(clusterList);
            cy.get('input[name="originZip"]').clear();
            cy.get('input[name="originZip"]').type(zipcode.zipCodes[i]);
            cy.get('input[value = "Update"]').click();
            cy.wait(5000);

            cy.get('table>tbody>tr>td.small').then(function (data) {

                clusterList = data.text().match(new RegExp('.{1,' + 13 + '}', 'g'));
                ProcessZipCluster(zipcode.zipCodes[i], clusterList);
            });
        }
                  
    })
})

function ProcessZipCluster(zipcode, data) {

    ZipList = [];
    data.forEach(cluster => {
        let zips = cluster.split("-");
        ZipList.push({ Zipcode: zipcode, StartZip: zips[0].trim(), EndZip: zips[1].trim() })
    });
    PostToDB(ZipList);

}
function GetZipCode() {
    cy
        .request('GET', 'http://localhost:60957/Home/GetZipcode')
        .then((response) => {
            console.log(response)
        })
}

function PostToDB(_data) {
    cy
        .request('POST', 'http://localhost:60957/Home/ImportOverNightMapData', _data )
        .then((response) => { 
            console.log(response);    
        })


}