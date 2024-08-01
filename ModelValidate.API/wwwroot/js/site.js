const uri = 'api/ValidateMessage/';
let todos = [];

function validateMessage() {
    debugger
    const inputString = document.getElementById('add-string');

    fetch(uri + 'DetectPhoneNumbers', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(inputString.value.trim())
    })
        .then(response =>
            response.json().then(data => ({
                data: data,
                status: response.status
            })
            ).then(res => {
                console.log(res.status, res.data)
                if (res.data.success)
                {
                    Swal.fire({
                        title: "Success!",
                        text: res.data.alertMessage,
                        icon: "success"
                    });
                }
                else{
                    Swal.fire({
                        title: "Error!",
                        text: res.data.alertMessage,
                        icon: "error"
                    });
                }

                _displayItems(res.data.data)
            }))
}

function _displayItems(data) {
    const tBody = document.getElementById('highlights');
    tBody.innerHTML = data;
}