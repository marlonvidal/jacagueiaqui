var map;

$(document).ready(function () {
    var mapa = document.getElementById("map-canvas");

    if (mapa != null) {
        var obj = RetornaLatLongCidade();
        var zoom = 6;
        var ehPaginaCagada = window.location.href.indexOf('/Cagada/') > -1;

        if (ehPaginaCagada) {
            zoom = 16;
        }

        var mapOptions = {
            zoom: zoom,
            center: new google.maps.LatLng(obj.lat, obj.long),
            disableDefaultUI: true,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            zoomControl: true,
            draggable: true
        }

        map = new google.maps.Map(mapa, mapOptions);

        if (!ehPaginaCagada) {

            var defaultBounds = new google.maps.LatLngBounds(
                new google.maps.LatLng(-19.975542697748352, -44.00140276396485),
                new google.maps.LatLng(-19.863214317717602, -43.893599419238285)
            );

            map.fitBounds(defaultBounds);
        }

        var input = document.getElementById('target');

        if (input != null) {
            var searchBox = new google.maps.places.SearchBox(input);

            google.maps.event.addListener(searchBox, 'places_changed', function () {
                var places = searchBox.getPlaces();
                var markers = [];

                for (var i = 0, marker; marker = markers[i]; i++) {
                    marker.setMap(null);
                }

                // For each place, get the icon, place name, and location.
                markers = [];
                var bounds = new google.maps.LatLngBounds();
                for (var i = 0, place; place = places[i]; i++) {
                    var image = {
                        url: place.icon,
                        size: new google.maps.Size(71, 71),
                        origin: new google.maps.Point(0, 0),
                        anchor: new google.maps.Point(17, 34),
                        scaledSize: new google.maps.Size(25, 25)
                    };

                    // Create a marker for each place.
                    var marker = new google.maps.Marker({
                        map: map,
                        icon: image,
                        title: place.name,
                        position: place.geometry.location
                    });

                    markers.push(marker);
                    bounds.extend(place.geometry.location);
                }

                map.fitBounds(bounds);
            });
        }
    }

    Modernizr.addTest('firefox_chrome', function () {
        return !!navigator.userAgent.match(/firefox/i) || !!navigator.userAgent.match(/chrome/i);
    });
});

// Function for adding a marker to the page.
function addMarker(location, arrastavel, animacao, id) {
    if (animacao) {
        return new google.maps.Marker({
            animation: google.maps.Animation.BOUNCE,
            title: 'Clique para mais detalhes',
            position: location,
            map: map,
            draggable: arrastavel,
            id: id
        });
    }
    else {
        return new google.maps.Marker({
            icon: '../Resources/merda-icone.png',
            title: 'Clique para mais detalhes',
            position: location,
            map: map,
            draggable: arrastavel,
            id: id
        });
    }
}

function RetornaLatLongBoundCidade() {

    var hiddenCidade = $('input[name=Cidade]');

    switch (hiddenCidade.val()) {
        case 'São Paulo':
            return new google.maps.LatLngBounds(
                new google.maps.LatLng(-23.625662594432096, -46.74130259765627),
                new google.maps.LatLng(-23.48183605175168, -46.59230052246096)
            );
        case 'Rio de Janeiro':
            return new google.maps.LatLngBounds(
                new google.maps.LatLng(-22.948602890131244, -43.40008555664065),
                new google.maps.LatLng(-22.780939595201122, -43.16525279296877)
            );
        default:
            return new google.maps.LatLngBounds(
                new google.maps.LatLng(-19.975542697748352, -44.00140276396485),
                new google.maps.LatLng(-19.863214317717602, -43.893599419238285)
            );
    }


}

function RetornaLatLongCidade() {
    var lat, long;

    var hiddenCidade = $('input[name=Cidade]');

    switch (hiddenCidade.val()) {
        case 'São Paulo':
            lat = -23.550149678471296;
            long = -46.636589157714866;
            break;
        case 'Rio de Janeiro':
            lat = -22.909078313278062;
            long = -43.175895798339866;
            break;
        default:
            lat = -19.9190657;
            long = -43.938574700000009;
            break;
    }

    return {
        lat: lat,
        long: long
    }
}

function bindInfoWindow(marker, map, infowindow, strDescription) {
    google.maps.event.addListener(marker, 'click', function () {
        infowindow.setContent(strDescription);
        infowindow.open(map, marker);
        $('#conteudo' + marker.id).parent().css('overflow', 'visible').css('width', '100%'); // parentNode.style.overflow = '';
    });
}

function Gostei(obj, id) {
    $.post('/RegistrarGostei?ID=' + id).done(function (json) {
        if (json) {
            $(obj).children().filter('span').text(json.total);
        }
    }).fail(function (err) {
        alert(err.responseText);
    });
}

function NaoGostei(obj, id) {
    $.post('/RegistrarNaoGostei?ID=' + id).done(function (json) {
        if (json) {
            $(obj).children().filter('span').text(json.total);
        }
    }).fail(function (err) {
        alert(err.responseText);
    });
}

function Comentarios(id) {
    var url = '/Cagada/' + id;
    if (Modernizr.firefox_chrome) {
        window.location = url;
    }
    else {
        window.location.href(url);
    }
}

function FuncoesPaginaIndex() {
    var url = $('input[name=Url]').val();

    var infowindow = new google.maps.InfoWindow({
        content: '',
        maxWidth: 700
    });

    $.post(url).done(function (json) {
        if (json && json.length > 0) {
            for (var i = 0; i < json.length; i++) {
                var location = new google.maps.LatLng(json[i].Lat, json[i].Long);
                var mensagem = "<div style='background-color: white;' id='conteudo" + json[i].ID + "'><div>";

                if (json[i].Onde == '' || json[i].Onde == null)
                    mensagem += json[i].Nome + " diz:</div> ";
                else
                    mensagem += json[i].Nome + " cagou em <b>" + json[i].Onde + "</b>:</div> ";

                mensagem += "<div>" + json[i].Comentario + "</div>";
                mensagem += "<br />";
                mensagem += "<div><a title='Gostei' onclick='Gostei(this, " + json[i].ID + ");'><img src='Resources/like.png' /><span>" + json[i].Gostei + "</span></a>";
                mensagem += "&nbsp;<a title='Não Gostei' onclick='NaoGostei(this, " + json[i].ID + ")'><img src='Resources/dislike.png' /><span>" + json[i].NaoGostei + "</span></a>";
                mensagem += "&nbsp;<a title='Comentários' onclick='Comentarios(" + json[i].ID + ")'><img src='Resources/comentar.png' alt='Comentários' />" + json[i].NumeroComentarios + "</a></div>";
                var marker = addMarker(location, false, false, json[i].ID);

                bindInfoWindow(marker, map, infowindow, mensagem);
            }
        }
    }).fail(function (err) {
        alert(err.responseText);
    });
}

function FuncoesPaginaCagada() {
    var id = parseInt($('input[id=post_ID]').val()),
    lat = parseFloat($('input[id=post_Lat]').val().replace(',', '.')),
    long = parseFloat($('input[id=post_Long]').val().replace(',', '.'));
    var location = new google.maps.LatLng(lat, long)

    var marker = addMarker(location, false, false, id);

    map.setCenter(marker.position);
    marker.setMap(map);
}

function FuncoesPaginaRegistrarCagada() {
    var coordCidade = RetornaLatLongCidade();
    var boundCidade = RetornaLatLongBoundCidade();

    ArmazenaLatLongObjeto(coordCidade.lat, coordCidade.long);

    var posicao = new google.maps.LatLng(coordCidade.lat, coordCidade.long);
    var marcador = addMarker(posicao, true, true);
    var markers = [];

    var defaultBounds = RetornaLatLongBoundCidade();

    map.fitBounds(defaultBounds);

    var input = document.getElementById('target');
    var searchBox = new google.maps.places.SearchBox(input);

    google.maps.event.addListener(searchBox, 'places_changed', function () {
        var places = searchBox.getPlaces();

        for (var i = 0, marker; marker = markers[i]; i++) {
            marker.setMap(null);
        }

        // For each place, get the icon, place name, and location.
        markers = [];
        var bounds = new google.maps.LatLngBounds();
        for (var i = 0, place; place = places[i]; i++) {
            var image = {
                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(25, 25)
            };

            // Create a marker for each place.
            var marker = new google.maps.Marker({
                map: map,
                icon: image,
                title: place.name,
                position: place.geometry.location
            });

            markers.push(marker);
            marcador.position = marker.position;
            ArmazenaLatLongObjeto(marcador.position.lat(), marcador.position.lng());

            bounds.extend(place.geometry.location);
        }

        map.fitBounds(bounds);
        map.setCenter(marcador.position);
    });

    google.maps.event.addListener(map, 'bounds_changed', function () {
        var bounds = map.getBounds();
        searchBox.setBounds(bounds);
    });

    google.maps.event.addListener(marcador, 'dragend', function (evt) {
        ArmazenaLatLongObjeto(evt.latLng.lat(), evt.latLng.lng());

        map.setCenter(marcador.position);
        marcador.setMap(map);
    });
}

var onSuccess = function (result) {
    if (result.url) {
        if (Modernizr.firefox_chrome) {
            window.location = result.url;
        }
        else {
            window.location.href(result.url);
        }
    }
}

function ArmazenaLatLongObjeto(lat, long) {
    $('input[name=Lat]').val(lat.toString().replace('.', ','));
    $('input[name=Long]').val(long.toString().replace('.', ','));
}

function final() {
    var nome = $('input[name=Nome]'),
    comentario = $('textarea[name=Comentario]');

    if(nome.val() != '' && comentario.val() != '') {
        alert('Comentário incluído com sucesso!');
        nome.val('');
        comentario.val('');
    }
}

function moveTela(id) {
    var p = $(id).position();
    $(window).scrollTop(p.top - 100);
}