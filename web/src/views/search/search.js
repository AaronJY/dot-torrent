import {computedFrom, inject} from 'aurelia-framework';
import { TorrentFinderHttpService } from "../../services/torrent-finder-http-service";

@inject(TorrentFinderHttpService)
export class Search {
  heading = 'Search';
  searchQuery = '';
  title = null;

  constructor(torrentFinderHttpService) {
    this.torrentFinderHttpService = torrentFinderHttpService;
  }

  activate() {
  
  }

  submit() {
    if (!this.searchQuery)
      return;
      
    this.torrentFinderHttpService.searchTitle(this.searchQuery)
      .then(title => {
        this.title = title;
        console.log(this.title);
      });
  }

  // canDeactivate() {
  //   if (this.fullName !== this.previousValue) {
  //     // eslint-disable-next-line no-alert
  //     return confirm('Are you sure you want to leave?');
  //   }
  // }
}

// export class UpperValueConverter {
//   toView(value) {
//     return value && value.toUpperCase();
//   }
// }
