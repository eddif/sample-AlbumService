var ViewModel = function () {
    var self = this;
    self.albums = ko.observableArray();
    self.error = ko.observable();

    var albumsUri = '/api/albums/'

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

    // Fetch
    getAllAlbums();
}

ko.applyBindings(new ViewModel());