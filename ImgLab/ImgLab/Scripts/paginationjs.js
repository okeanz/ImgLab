var page = 1;
var Names;
var Dates;

function imageContainer(names, start, count) {
    if (Names == undefined)
        Names = names
    if (names.length == 0 || names == null) return '';

    var DNames = names.slice(start, start+count);
    //alert('d:' + DNames.length + ' n:' + Names.length + ' s:'+start+' c:'+count + ' nn:'+names.length);

    var html = '<div class="container-fluid">';
    for (var i = 0; i < DNames.length; i += 3) {
        html += '<div class="row">';
        for (var k = i; k < i + 3; k++) {
            if (k < DNames.length && DNames[k] != null) {
                html += '<div class="col-md-4">'
                html += '<div class="thumbnail">';
                html += '<img src="/Home/GetImage?name=' + DNames[k] + '" alt="img" class="img-responsive img-thumbnail" />';
                html += '<div class="caption">' + DNames[k] + '</div>';
                html += '<div class="caption">Creation Date: ' + Dates[k] + '</div>';
                html += '</div>';
                html += '</div>';
            }
        }
        html += '</div>';
    }
    html += '</div>'

    var pages = Math.ceil(names.length / count);
    

    html += '<ul class="pagination">';

    html += '<li><a onClick="Page(' + 0 + ',' + count + ')" href="#">'+'<<'+'</a></li>';
    for (var i = 1; i < pages+1; i++) {
        if (page == i)
            html += '<li class="active"><a href="#">' + (i) + '</a></li>';
        else
            html += '<li><a onClick="Page(' + (i-1) + ',' + count + ')" href="#">' + (i) + '</a></li>';
        
    }
    html += '<li><a onClick="Page(' + (pages-1) + ',' + count + ')" href="#">' + '>>' + '</a></li>';
    html += '</ul>';

    var div = document.getElementsByClassName('main')[0];
    div.innerHTML = html;
}

function Page(nPage, count) {
    
    page = nPage+1;
    clearMainDiv();
    imageContainer(Names, count * nPage, count);
}

function clearMainDiv() {
    var div = document.getElementsByClassName('main')[0];
    while (div.firstChild)
        div.removeChild(div.firstChild);
}
