// import zipcode from '../../fixtures/zipCode.json'
// import zipCodes from '../fixtures/zipCode.json'
import zipcode from '../fixtures/zipCode.json'

// clusterList = {};

describe('sample', function () {
    it('sample', function () {
        var count = 0;
        cy.visit('http://www.fedex.com/grd/maps/MapEntry.do');

        for (let i = 0; i < zipcode.zipCodes.length; i++) {
            var clusterList = [];
            if (!i) {
                cy.get('input[name="originZip"]').type(zipcode.zipCodes[i]);
                cy.get('select[name="resType"]').select('YES');
                cy.get('input[value="View overnight map"]').click();
                cy.wait(5000);
                cy.get('table>tbody>tr>td.small').each(($data, index, $list) => {
                    // console.log($data, index, $list);
                    clusterList.push($data.text());
                    if (index == ($list.length - 1))
                        ProcessZipCluster(zipcode.zipCodes[i], clusterList);
                });
            } else {
                cy.get('input[name="originZip"]').clear();
                cy.get('input[name="originZip"]').type(zipcode.zipCodes[i]);
                cy.get('input[value = "Update"]').click();
                // cy.wait(5000);
                cy.get('table>tbody>tr>td.small').each(($data, index, $list) => {
                    clusterList.push($data.text());
                    if (index == ($list.length - 1))
                        ProcessZipCluster(zipcode.zipCodes[i], clusterList);
                });
                // ProcessZipCluster(zipcode.zipCodes[i], clusterList);
            }
        }
    })
});

function ProcessZipCluster(zipcode, data) {
    // console.log('zipcode', zipcode, 'no of records', data.length);
    data.forEach(cluster => {
        let zips = cluster.split("-");
        console.log();
        PostToDB({ Zipcode: zipcode, StartZip: zips[0].trim(), EndZip: zips[1].trim() });
    });
}

function PostToDB(_data) {
    //fetch('http://localhost:60957/Home/ImportOverNightMapData', {
    //    method: "POST",
    //    body: JSON.stringify(_data),
    //    headers: {
    //        'Accept': 'application/json; charset=utf-8',
    //        'Content-Type': 'application/json;charset=UTF-8'
    //    }
    //})
    //    .then(
    //        response => response.json()
    //        , err => console.log(err)
    //);

    cy
        .request('POST', 'http://localhost:60957/Home/ImportOverNightMapData', { Zipcode: _data.Zipcode, StartZip: _data.StartZip, EndZip: _data.EndZip })
        .then((response) => {
            // response.body is automatically serialized into JSON
            //expect(response.body).to.have.property('name', 'Jane') // true
            console.log(response);
        })


    //     .then(json => console.log(json));
    // .catch (err => console.log(err));
    // });
}