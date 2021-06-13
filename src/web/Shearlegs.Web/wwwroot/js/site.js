function ShowModal(id) {
    var myModal = new bootstrap.Modal(document.getElementById(id));
    myModal.show();
}

function ShowModalStatic(id) {
    var myModal = new bootstrap.Modal(document.getElementById(id), {
        backdrop: 'static'
    });
    myModal.show();
}

function HideModal(id) {
    var myModalEl = document.getElementById(id);
    var modal = bootstrap.Modal.getInstance(myModalEl);
    modal.hide();
}

window.ChangeUrl = function (url) {
    history.pushState(null, '', url);
}

async function GetFormDataJson(formName) {
    let form = document.forms[formName];

    let fd = new FormData(form);

    let data = {};

    for (let [key, prop] of fd) {

        console.log(key);
        console.log(prop);

        if (prop instanceof File) {
            data[key] = Array.from(new Uint8Array(await readFileAsData(prop)));

        } else {
            data[key] = prop;
        }

    }

    data = JSON.stringify(data, null, 2);

    return data;
}