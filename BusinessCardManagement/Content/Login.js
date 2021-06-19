var host = "https://" + window.location.host; 

$("#LogIn").click(
	function () {
		var email = $("#email").val();
		var password = $("#password").val();
		if (email == "") {
			return Swal.fire({
				icon: 'error',
				title: 'Oops...',
				text: 'Email is required!',
			})
		}
		if (password == "") {
			return Swal.fire({
				icon: 'error',
				title: 'Oops...',
				text: 'Password is required!',
			})
		}
		if (!validateEmail(email)) {
			return Swal.fire({
				icon: 'error',
				title: 'Oops...',
				text: 'Email not valid!',
			})
		}
		$.ajax({
			method: "POST",
			url: host +'/User/Login',
			data: {
				action: "login",
				email: email.trim(),
				password: password,
			}

		}).done(function (data) {
			console.log(data);
			if (data.result == "false") {
				return Swal.fire({
					icon: 'error',
					title: 'Oops...',
					text: data.result,
				})
			} else {
				location.reload();

			}


			


		});
	}

);
function validateEmail(email) {
	const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
	return re.test(String(email).toLowerCase());
}