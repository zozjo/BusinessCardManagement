var host = "https://" + window.location.host;

$("#PicImage").click(() => {
    $("#Image").click();
});
function encodeImgtoBase64(element) {
    
    var file = element.files[0];
    var reader = new FileReader();
    reader.onloadend = function () {

        $("#PicImage").attr("src", reader.result);

    }
    reader.readAsDataURL(file);
}
function checkfile(sender) {
    var validExts = new Array(".xlsx", ".xls", ".csv");
    var fileExt = sender.value;
    fileExt = fileExt.substring(fileExt.lastIndexOf('.'));
    fileExt = fileExt.toLowerCase()
    if (fileExt == ".png" || fileExt == ".jpg" || fileExt == ".jpeg") {
        var file = sender.files[0];
        var reader = new FileReader();
        reader.onloadend = function () {

            $.ajax({
                method: "POST",
                url: host +'/Home/ReaderQrCode',
                type: 'POST',
                data: {
                    base64String: reader.result.replace("data:image/png;base64,", ""),
                }
            }).done(function (data) {
                console.log(data.Address);
                $("#exampleModal").modal("show");
                $("#Gender").val(data.Gender);
                $("#DateOfBirth").val(data.DateOfBirth);
                $("#Email").val(data.Email);
                $("#Phone").val(data.Phone);
                $("#Address").val(data.Address);
                $("#Name").val(data.Name);

            });

        }
        reader.readAsDataURL(file);
    } else if (fileExt == ".xlsx" || fileExt == ".xls" || fileExt == ".csv") {
        var fd = new FormData();
        var files = $('#file')[0].files;
        fd.append('postedFile', files[0]);

        $.ajax({
            method: "POST",
            url: host +'/Home/ReadCSV',
            type: 'post',
            data: fd,
            contentType: false,
            processData: false,


        }).done(function (data) {
            console.log(data[1]);
            data = data[1];
            console.log(data.Address);
            $("#exampleModal").modal("show");
            $("#Gender").val(data.Gender);
            $("#DateOfBirth").val(data.DateOfBirth);
            $("#Email").val(data.Email);
            $("#Phone").val(data.Phone);
            $("#Address").val(data.Address);
            $("#Name").val(data.Name);
            $("#PicImage").attr("src","data:image/png;base64,"+data.Photo)
        });
    }
    else if (fileExt == ".xml") {
        var fd = new FormData();
        var files = $('#file')[0].files;
        fd.append('postedFile', files[0]);

        $.ajax({
            method: "POST",
            url: host +'/Home/ReadXML',
            type: 'post',
            data: fd,
            contentType: false,
            processData: false,


        }).done(function (data) {
            console.log(data);
            $("#exampleModal").modal("show");
            $("#Gender").val(data.Gender);
            $("#DateOfBirth").val(data.DateOfBirth);
            $("#Email").val(data.Email);
            $("#Phone").val(data.Phone);
            $("#Address").val(data.Address);
            $("#Name").val(data.Name);
            $("#PicImage").attr("src", "data:image/png;base64," + data.Photo)
        });
    } else {
        alert("Selected file is not valid");
        
        document.getElementById('file').value = null;

    }
   
}
$("#SaveNewCard").click(() => {
    var Gender= $("#Gender").val();
    var DateOfBirth = $("#DateOfBirth").val();
    var Email =$("#Email").val();
    var Phone = $("#Phone").val();
    var Address =$("#Address").val();
    var Name =$("#Name").val();
    var PicImage = $("#PicImage").attr("src");

    if (Name == "") {
        return swal("Oops...", "Name is required!", "error");

    }
    if (Gender == "") {
        return swal("Oops...", "Gender is required!", "error");
    }
    if (DateOfBirth == "") {
        return swal("Oops...", "Date Of Birth is required!", "error");
    }
    if (Email == "") {
        return swal("Oops...", "Email is required!", "error");
    }
    if (Phone == "") {
        return swal("Oops...", "Phone is required!", "error");
    }
    if (Address == "") {
        return swal("Oops...", "Address is required!", "error");
    }
    if (PicImage == "/Content/Images/user.png") {
        PicImage = "";
    }
    $.ajax({
        method: "POST",
        url: host + '/Home/InsertBusinessCard',
        type: 'POST',
        data: {
            Name: Name,
            Gender: Gender,
            DateOfBirth: DateOfBirth,
            Email: Email,
            Phone: Phone,
            Photo: PicImage.replace("data:image/png;base64,",""),
            Address: Address

        }
    }).done(function (data) {
        if (data == "Done") {
            $("#CancelNewCard").click();
            swal({
                title: "Good job!",
                text: "Your business card add successfully!",
                icon: "success",
            });
        }

    });

})
$("#CancelNewCard").click(() => {

    $("#Gender").val("");
    $("#DateOfBirth").val("");
    $("#Email").val("");
    $("#Phone").val("");
    $("#Address").val("");
     $("#Name").val("");
    $("#PicImage").attr("src","/Content/Images/user.png");
})