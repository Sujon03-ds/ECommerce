
//Alert box 
function Agreement()
{
    $.alert({
        title: '<span style="color:green">Agreement</span>',
        icon: 'fas fa-handshake',
        type: 'green',
        draggable: true,
        useBootstrap: false,
        content: '<p style="text-align: justify;"><b>"eCounting"</b> has brought a revolution for SME sector. It was launched on March 01, 2018. It is a cloud based software solution with set of features, reports, statistical dashboard and centralized operation facility. Its an integrated <b>Accounting</b> and <b>Inventory</b> Management Software.</p>',
    });
}
function CheckAgreement() {
        Agreement();
}
function Rememberme() {

    if ($('#RememberMe').is(':checked')) {
        $("#RememberMe").prop('checked', false)

    }
    else {

        $("#RememberMe").prop('checked', true)
    }
}

$(document).ready(function () {
    //Loading();
    //$(".divLoader").hide();
});
function Loading() {
    $(".divLoader").show();
}
function Closing() {
    $(".divLoader").hide();
}
function Hideme() {

    if ($('.hideme').val() == 0) {
        $('#divDeshoardContainer').hide();
        $('.hideme').val(1);
    }
    else {
        $('#divDeshoardContainer').show();
        $('.hideme').val(0);
    }
}
function MESSENGER() {
    
    var id = $('.msgBox').attr("id");
    var htm = $('.msgBox').html();
    debugger;
    if (id > 0) {
        if (id == 1 || id == 2 || id == 3 || id == 6) {

            $.alert({
                title: '<span style="color:green">success</span>',
                icon: 'fa fa-check',
                type: 'green',
                draggable: true,
                boxWidth: '400px',
                useBootstrap: false,
                content: htm,
            });


        }
        else {
            if (id == 5) {
                $.alert({
                    title: 'warning',
                    icon: 'fa fa-warning',
                    type: 'yellow',
                    draggable: true,
                    boxWidth: '400px',
                    useBootstrap: false,
                    content: htm,
                });
            }
            else {
                $.alert({
                    title: 'error',
                    icon: 'fa fa-warning',
                    type: 'red',
                    draggable: true,
                    boxWidth: '400px',
                    useBootstrap: false,
                    content: htm,
                });

            }
        }
    }


}
function INVALIDIMAGE(msg) {
    $.alert({
        title: 'error',
        icon: 'fa fa-warning',
        type: 'red',
        draggable: true,
        boxWidth: '400px',
        useBootstrap: false,
        content: '<span style="color:red;font-weight:bold;font-family: initial; font-size:16px;">' + msg + '</span>',
    });

}
function DeleteItem(obj)
{
    debugger;
    $.confirm({
        title: 'Are you sure to delete this item?',
        content:null,
        icon: 'fa fa-warning',
        type: 'red',
        typeAnimated: true,
        buttons: {
            No: {
                text: 'No',
                btnClass: 'btn-green',
                action: function () {
                    
                }
            },
            Yes: function () {
                document.location = obj;
            }
        }
    });
    return false;
    
}

