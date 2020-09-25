

 var clusterList = [];
describe('sample', function () {
    it('sample', function () {
        ResetZipcode();
    });
});
 
   function GetZipCode() {
    cy
        .request('GET', 'https://milliardswhobackend.azurewebsites.net/api/ZipCluster/getzipcode')
        .then((response) => {
            let zip =response.body;
            cy.get('input[name="originZip"]').clear();
            cy.get('input[name="originZip"]').type(zip);;
            cy.get('input[value = "Update"]').click();
            cy.get('body').then(($body) => {
                if ($body.text().includes('View Outbound Map')) {
                    ResetZipcode();
                } else {
                    cy.get('table>tbody>tr>td.small').then(function (data) {
                        clusterList = data.text().match(new RegExp('.{1,' + 13 + '}', 'g'));
                        ProcessZipCluster(zip, clusterList);
                    });
                }
            })
        });  
}

function ProcessZipCluster(zipcode, data) {
   var ZipList = [];
    data.forEach(cluster => {
        let zips = cluster.split("-");
        ZipList.push({ Zipcode: zipcode, StartZip: zips[0].trim(), EndZip: zips[1].trim() })
    });
    console.log(ZipList);
    PostToDB(ZipList);
}

function ResetZipcode() {
    cy.visit('http://www.fedex.com/grd/maps/MapEntry.do');
    cy.get('input[name="originZip"]').type("10038");
    cy.get('select[name="resType"]').select('YES');
    cy.get('input[value="View overnight map"]').click();
    cy.wait(5000);
    GetZipCode();
}

function PostToDB(_data) {
    cy
        .request('POST', 'https://milliardswhobackend.azurewebsites.net/api/ZipCluster/updateovernightmap', _data )
        .then((response) => { 
            console.log(response); 
            GetZipCode();           
        })
}


