import zipcode from '../fixtures/zipCode.json'
describe('sample', function () {

    var clusterList = [];
    it('sample', function () {
        var count = 0;
        cy.visit('http://www.fedex.com/grd/maps/MapEntry.do');
        cy.get('input[name="originZip"]').type('07102');
        cy.get('select[name="resType"]').select('YES');
        cy.get('input[value="View overnight map"]').click();
        cy.wait(5000);
        cy.get('table>tbody>tr>td.small').each(($data, index, $list) => {
            console.log($data.text());
        });
        for (let i = 0; i < zipcode.zipCodes.length; i++) {
            clusterList = [];
            cy.get('input[name="originZip"]').clear();
            cy.get('input[name="originZip"]').type(zipcode.zipCodes[i]);
            cy.get('input[value = "Update"]').click();
            cy.wait(5000);

            cy.get('table>tbody>tr>td.small').then(function (data) {

                clusterList = data.text().match(new RegExp('.{1,' + 13 + '}', 'g'));
                console.log(data.text());
                console.log(clusterList);
            });

            //cy.get('table>tbody>tr>td.small').each(($data, index, $list) => {
            //    console.log($data.text());
            //   // clusterList.push($data.text());
            //   // console.log(clusterList)
             
            //  //  console.log(clusterList)
           
            //   // console.log(count++)
            //});
         
        }
    })
})