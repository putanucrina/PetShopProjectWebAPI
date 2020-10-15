$(() => {

    const table = $('.table'),
        tableBody = table.find('tbody'),
        searchInput = $('#searchInput'),
        searchButton = $('button[type="button"]');

    function renderTable(users /*[]*/) {

        tableBody.empty();

        for (var i = 0; i < users.length; i++) {
            const user = users[i],
                dataNastere = user.DataNastere.toDate();

            const element = $(`
                <tr>

                    <td>
                        ${user.Nume}
                    </td>
                    <td>
                        ${user.Prenume}
                    </td>
                    <td>
                        ${user.Telefon}
                    </td>
                    <td>
                        ${dataNastere.toUserDate()}
                    </td>
                    <td>
                        ${user.Username}
                    </td>
                    <td>
                        <a href="/Administration/Edit/${user.Id}">Edit</a>
                    </td>
                </tr>
            `);

            tableBody.append(element);
        }
    }

    function search(value) {
        $.ajax({
            url: getUsersUrl,
            method: 'GET',
            data: {
                search: value
            },
            success: function (users) {
                renderTable(users);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    search();

    searchButton.click(() => {
        const value = searchInput.val();

        search(value);
    });
});