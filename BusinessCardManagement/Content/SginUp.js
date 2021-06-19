var host = "https://" + window.location.host; 

$("#Register").click(
	function () {
		var email = $("#email").val();
		var password = $("#password").val();
		var confirm_password = $("#confirm-password").val();
		var name = $("#name").val();
		if (name == "") {
			return Swal.fire({
				icon: 'error',
				title: 'Oops...',
				text: 'Full Name is required!',
			})
		}
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
		if (confirm_password == "") {
			return Swal.fire({
				icon: 'error',
				title: 'Oops...',
				text: 'Confirm Password is required!',
			})
		}
		if (confirm_password != password) {
			return Swal.fire({
				icon: 'error',
				title: 'Oops...',
				text: 'Password And Confirm Password Not Match',
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
			url: host + '/User/SginUp',
			data: {
				email: email,
				password: password,
				username: name
			}

		}).done(function (data) {
			console.log(data);
			if (data.result=="false") {
				return Swal.fire({
					icon: 'error',
					title: 'Oops...',
					text: data.msg,
				})
			} else {
				return Swal.fire({
					icon: 'success',
					title: 'Good job',
					text: "your account registered successfully , back to login page",
				}).then((result) => {
					/* Read more about isConfirmed, isDenied below */
					if (result.isConfirmed) {
						location.replace(host+"/User")
					} 
				})
            }

		});
	}

);
function validateEmail(email) {
	const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
	return re.test(String(email).toLowerCase());
}