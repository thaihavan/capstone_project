/// <reference types="@types/googlemaps" />
import { Component, OnInit, Output, EventEmitter, ViewChild, AfterViewInit, Input } from '@angular/core';
/// <reference path="<relevant path>/node_modules/@types/googlemaps/index.d.ts" />
import {MapsAPILoader} from '@agm/core';
@Component({
  selector: 'app-google-map-search',
  templateUrl: './google-map-search.component.html',
  styleUrls: ['./google-map-search.component.css']
})

export class GoogleMapSearchComponent implements OnInit, AfterViewInit {
  textInput = '';
  constructor(private mapLoader: MapsAPILoader) {
  }
  @ViewChild('searchmap') inputSearch;
  // tslint:disable-next-line:no-output-on-prefix
  @Output() onSelect: EventEmitter<any> = new EventEmitter();
  @Input() width: number;
  @Input() height: number;
  @Input() matFill = false;
  isShow = true;
  private element: HTMLInputElement;
  ngOnInit(): void {
  }
  ngAfterViewInit(): void {
      this.element = this.inputSearch.nativeElement;
      this.mapLoader.load().then(() => {
        const autocomplete = new google.maps.places.Autocomplete(this.element);
        google.maps.event.addListener(autocomplete, 'place_changed', () => {
          this.getFormatPlace(autocomplete.getPlace());
          this.onSelect.emit(this.getFormatPlace(autocomplete.getPlace()));
        });
      });
  }
  getFormatPlace(place) {
    const locationObj = {};
    // tslint:disable-next-line:forin
    for (const i in place.address_components) {
      const item = place.address_components[i];
      let name: string;
      name = this.inputSearch.nativeElement.value;
      name = name.slice(0, name.indexOf(','));
      // tslint:disable-next-line:no-string-literal
      locationObj['formatted_address'] = place.formatted_address;
      // tslint:disable-next-line:no-string-literal
      locationObj['name'] = name;
      // tslint:disable-next-line:no-string-literal
      locationObj['locationId'] = place.id;
      // tslint:disable-next-line:no-string-literal
      locationObj['icon'] = place.icon;
      // tslint:disable-next-line:no-string-literal
      if (place.photos != null) {
        // tslint:disable-next-line:no-string-literal
      locationObj['image'] = place.photos[0].getUrl();
      }
      if (item.types.indexOf('locality') > -1) {
        // tslint:disable-next-line:no-string-literal
        locationObj['locality'] = item.long_name;
      } else if (item.types.indexOf('administrative_area_level_1') > -1) {
        // tslint:disable-next-line:no-string-literal
        locationObj['admin_area_l1'] = item.short_name;
      } else if (item.types.indexOf('street_number') > -1) {
        // tslint:disable-next-line:no-string-literal
        locationObj['street_number'] = item.short_name;
      } else if (item.types.indexOf('route') > -1) {
        // tslint:disable-next-line:no-string-literal
        locationObj['route'] = item.long_name;
      } else if (item.types.indexOf('country') > -1) {
        // tslint:disable-next-line:no-string-literal
        locationObj['country'] = item.long_name;
      } else if (item.types.indexOf('postal_code') > -1) {
        // tslint:disable-next-line:no-string-literal
        locationObj['postal_code'] = item.short_name;
      }
      this.textInput = '';
      // tslint:disable-next-line:no-string-literal
    }
    // tslint:disable-next-line:no-string-literal
    locationObj['lat'] = place.geometry.location.lat();
    // tslint:disable-next-line:no-string-literal
    locationObj['lng'] = place.geometry.location.lng();
    return locationObj;
  }
}

