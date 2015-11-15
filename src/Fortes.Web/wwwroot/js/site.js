function alertaErro(obj, msg) {
    var div = "<div class=\"alert alert-danger alert-dismissible\" role=\"alert\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" + msg + "</div>";
    $(div).prependTo(obj);
}
function alertaSucesso(obj, msg) {
    var div = "<div class=\"alert alert-success alert-dismissible\" role=\"alert\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" + msg + "</div>";
    $(div).prependTo(obj);
}
function exportarPdf(id) {
    $(id).tableExport({ type: 'pdf', escape: 'true' });
}