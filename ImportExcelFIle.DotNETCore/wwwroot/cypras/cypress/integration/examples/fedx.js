import zipcode from '../../fixtures/zipCode.json'

describe('sample', function () {
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

        var 



        
        for (let i = 0; i < zipcode.zipCodes.length; i++) {
            cy.get('input[name="originZip"]').clear();
            cy.get('input[name="originZip"]').type(zipcode.zipCodes[i]);
            cy.get('input[value = "Update"]').click();
           // cy.wait(5000);
            cy.get('table>tbody>tr>td.small').each(($data, index, $list) => {
                   
                console.log($data.text());
                console.log(count++)

            });




            //var fso = new ActiveXObject("Scripting.FileSystemObject");
            //var a = fso.CreateTextFile("c:\\testfile.txt", true);
            //a.WriteLine("This is a test.");
            //a.Close();
        }
    })
})