
$(document).ready(function () {
    $(function () {
        $("#DDColecciones").select2();
    })

    function manejarErrores(err) {
        console.log(err.responseText);
    }
})