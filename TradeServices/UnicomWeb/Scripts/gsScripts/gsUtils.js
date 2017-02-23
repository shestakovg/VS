
function getBaseUrl() {
    var re = new RegExp(/^.*\//);
    return re.exec(window.location.href);
}