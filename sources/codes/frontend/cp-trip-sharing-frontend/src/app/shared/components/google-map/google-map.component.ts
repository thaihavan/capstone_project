import {
  Component,
  OnInit,
  NgZone,
  Output,
  EventEmitter,
  Input,
  AfterViewInit,
  OnChanges
} from '@angular/core';
import { LocationMarker } from 'src/app/model/LocationMarker';

@Component({
  selector: 'app-google-map',
  templateUrl: './google-map.component.html',
  styleUrls: ['./google-map.component.css']
})
export class GoogleMapComponent implements OnInit, AfterViewInit, OnChanges {
  @Output() addDestination = new EventEmitter();
  lat: number;
  lng: number;
  isFirstTime: boolean;
  public addrKeys: string[];
  public addr: object;
  @Input() userRole;
  @Input() heightMap: any;
  @Input() locationMarker: LocationMarker[] = [];
  @Input() isCreate: boolean;
  constructor(private zone: NgZone) {}

  ngOnInit() {
    this.isFirstTime = this.isCreate;
  }

  ngAfterViewInit(): void {
    setTimeout(() => {
      if (this.isCreate) {
        this.locationMarker = [];
        this.setCurrentLocation();
      }
    }, 1000);
  }

  ngOnChanges(): void {}

  // on google-map-search submit add address location.
  setAddress(addrObj) {
    this.zone.run(() => {
      this.addr = addrObj;
      this.addrKeys = Object.keys(addrObj);
      const location: LocationMarker = {
        longitude: addrObj.lng,
        latitude: addrObj.lat,
        formattedAddress: addrObj.formatted_address,
        locationId: addrObj.locationId,
        icon: addrObj.icon,
        image: addrObj.image,
        name: addrObj.name,
        note: ''
      };
      if (this.isFirstTime) {
        this.locationMarker.pop();
        this.isFirstTime = false;
      }
      this.locationMarker.push(location);
      this.lat = addrObj.lat;
      this.lng = addrObj.lng;
    });
    this.addDestination.emit(this.locationMarker);
  }

  // get your current location
  private setCurrentLocation() {
    if ('geolocation' in navigator) {
      navigator.geolocation.getCurrentPosition(position => {
        const location: LocationMarker = {
          longitude: position.coords.longitude,
          latitude: position.coords.latitude,
          formattedAddress: 'VN',
          locationId: '',
          icon: '',
          image: '',
          name: '',
          note: ''
        };
        this.locationMarker.push(location);
        this.lat = position.coords.latitude;
        this.lng = position.coords.longitude;
      });
    }
  }

  // fitbound google maps
  fitBound() {
    if (this.locationMarker === undefined) {
      return false;
    } else {
      if (this.lat === this.locationMarker[0].latitude && this.lng === this.locationMarker[0].longitude) {
        return false;
      }
      return this.locationMarker.length > 0;
    }
  }

  // mouse over marker
  onMouseOver(infoWindow, gm) {

    if (gm.lastOpen != null) {
        gm.lastOpen.close();
    }

    gm.lastOpen = infoWindow;

    infoWindow.open();
}
}
