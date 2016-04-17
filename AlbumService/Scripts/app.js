var ViewModel = function () {
    var self = this;
    self.albums = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.artists = ko.observableArray();

    // View Album
    var albumsUri = '/api/albums/'
    var artistsUri = '/api/artists/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllAlbums() {
        ajaxHelper(albumsUri, 'GET').done(function (data) {
            self.albums(data);
        });
    }

    self.getAlbumDetail = function (item) {
        ajaxHelper(albumsUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    // Add new Album
    self.newAlbum = {
        Artist: ko.observable(),
        Genre: ko.observable(),
        Price: ko.observable(),
        Title: ko.observable(),
        Year: ko.observable()
    }

    function getArtists() {
        ajaxHelper(artistsUri, 'GET').done(function (data) {
            self.artists(data);
        });
    }

    self.addAlbum = function (formElement) {
        var album = {
            ArtistId: self.newAlbum.Artist().Id,
            Genre: self.newAlbum.Genre(),
            Price: self.newAlbum.Price(),
            Title: self.newAlbum.Title(),
            Year: self.newAlbum.Year()
        };

        ajaxHelper(albumsUri, 'POST', album).done(function (item) {
            self.albums.push(item);
           // self.albums.push({ Id: item.Id, Title: item.Title, ArtistName: item.Artist.Name });
        });
    }

    // Fetch
    getAllAlbums();
    getArtists();

}

ko.applyBindings(new ViewModel());