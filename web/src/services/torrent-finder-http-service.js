import {inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';

@inject(HttpClient)
export class TorrentFinderHttpService {
  constructor(http) {
    http.configure(config => {
      config
        .useStandardConfiguration()
        .withBaseUrl('https://localhost:44398/api/');
    });

    this.http = http;
  }

  searchTitle(title) {
    var urlEncodedTitle = encodeURIComponent(title);
    return this.http.fetch(`search/title/${urlEncodedTitle}`)
      .then(response => response.json());
  }

  searchImdbId(id) {
    return this.http.fetch(`search/id/${id}`)
      .then(response => response.json());
  }
}