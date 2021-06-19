var host = "https://" + window.location.host;

initAjax()
var isabblyFillter = false;
function initAjax() {
    $.ajax({
        method: "POST",
        url: host + '/Home/GetBusinessCard',
    }).done(function (data) {
        console.log(data);
        if (data != null) {
            var carditem = "";
            for (var i = 0; i < data.length; i++) {
                carditem += ' <div class="col-md-6" id="Card' + data[i].BusinessCardID + '">'
                    + ' <div class="">'
                    + '  <div class="card" onclick="onclick">'
                    + ' <div class="inner">'
                    + ' <div class="front">'
                if (data[i].Photo != "" && data[i].Photo != null) {
                    carditem += "<img src='data:image/png;base64," + data[i].Photo + "' />"
                }
                carditem += ' <h1>' + data[i].Name + '</h1>'
                    + '</div>'
                    + ' <div class="back">'
                    + '<h1>' + data[i].Name + '</h1>'
                    + '<p> Email: ' + data[i].Email + '</p>'
                    + '<p> Phone: ' + data[i].Phone + '</p>'
                    + '<p> Date Of Birth: ' + data[i].DateOfBirth + '</p>'
                    + '<ul class="flex-container space-between">'
                    + ' <li class="flex-item">Address: ' + data[i].Address + '</li>'
                    + ' <li class="flex-item">Gender: ' + data[i].Gender + '</li>'
                    + '</ul>'
                    + '<div class="flex-container space-between m-t-20">'
                    + '<div class="iconBoxCard deleteIcon" onclick="deleteCard(' + data[i].BusinessCardID + ')"><i class="fas fa-trash"></i></div>'
                    + '<a class="iconBoxCard" href="ExportBusinessCardToCSV/' + data[i].BusinessCardID + '">CSV</a>'
                    + '<a class="iconBoxCard" href="ExportToXML/' + data[i].BusinessCardID + '">XML</a>'
                    + ' </div>'
                    + ' </div>'
                    + ' </div>'
                    + '</div>'
                    + ' </div>'
                    + '</div>';
            }
            $("#cards").append(carditem);
        }

    });
}


function deleteCard(CardID) {
    Swal.fire({
        title: 'Do you want to delete the card?',
        showDenyButton: true,
        showCancelButton: false,
        confirmButtonText: `Delete`,
        confirmButtonColor: '#000000',
        denyButtonColor: '#797979',
        denyButtonText: `Don't delete`,
    }).then((result) => {

        if (result.isConfirmed) {
            $.ajax({
                method: "POST",
                url: host + '/Home/DeleteCard',
                data: {
                    CardID: CardID,
                }
            }).done(function (data) {
                console.log(data);
                if (data == "true") {
                    $("#Card" + CardID).hide();
                }
            });

        } else if (result.isDenied) {
            Swal.fire('Changes are not saved', '', 'info')
        }
    })
}

$(document).on('input', '#Search', function () {
    if ($('#Search').val() == "") {
        if (isabblyFillter == true) {
            $('#cards').empty();
            initAjax();
            isabblyFillter = false;
        }
    }
});
$(document).on('click', '#SearchIcons', function () {

    if ($('#Search').val() == "") {
        if (isabblyFillter == true) {
            $('#cards').empty();
            initAjax();
        }

    }
    else {
        $('#cards').empty();
        $.ajax({
            method: "POST",
            url: host + '/Home/GetBusinessCardFilter',
            data: {
                Filter: $('#Search').val(),
            }
        }).done(function (data) {
            console.log(data);
            if (data != null) {
                var carditem = "";
                for (var i = 0; i < data.length; i++) {
                    carditem += ' <div class="col-md-6" id="Card' + data[i].BusinessCardID + '">'
                        + ' <div class="">'
                        + '  <div class="card" onclick="onclick">'
                        + ' <div class="inner">'
                        + ' <div class="front">'
                    if (data[i].Photo != "" && data[i].Photo != null) {
                        carditem += "<img src='data:image/png;base64," + data[i].Photo + "' />"
                    }
                    carditem += ' <h1>' + data[i].Name + '</h1>'
                        + '</div>'
                        + ' <div class="back">'
                        + '<h1>' + data[i].Name + '</h1>'
                        + '<p> Email: ' + data[i].Email + '</p>'
                        + '<p> Phone: ' + data[i].Phone + '</p>'
                        + '<p> Date Of Birth: ' + data[i].DateOfBirth + '</p>'
                        + '<ul class="flex-container space-between">'
                        + ' <li class="flex-item">Address: ' + data[i].Address + '</li>'
                        + ' <li class="flex-item">Gender: ' + data[i].Gender + '</li>'
                        + '</ul>'
                        + '<div class="flex-container space-between m-t-20">'
                        + '<div class="iconBoxCard deleteIcon" onclick="deleteCard(' + data[i].BusinessCardID + ')"><i class="fas fa-trash"></i></div>'
                        + '<a class="iconBoxCard" href="ExportBusinessCardToCSV/' + data[i].BusinessCardID + '">CSV</a>'
                        + '<a class="iconBoxCard" href="ExportToXML/' + data[i].BusinessCardID + '">XML</a>'
                        + ' </div>'
                        + ' </div>'
                        + ' </div>'
                        + '</div>'
                        + ' </div>'
                        + '</div>';
                }
                $("#cards").append(carditem);
                isabblyFillter = true;
            }

        });

    }
});
