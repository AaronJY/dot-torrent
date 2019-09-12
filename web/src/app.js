import {PLATFORM} from 'aurelia-pal';

export class App {
  configureRouter(config, router) {
    config.title = 'DotTorrent';
    config.map([
      {
        route: ['', 'search'],
        name: 'search',
        moduleId: PLATFORM.moduleName('./views/search/search'),
        nav: true,
        title: 'Search'
      }
    ]);

    this.router = router;
  }
}
