import {
  Component,
  OnInit,
  NgZone,
  Output,
  EventEmitter,
  Input
} from '@angular/core';
import { LocationMarker } from 'src/app/model/LocationMarker';

@Component({
  selector: 'app-google-map',
  templateUrl: './google-map.component.html',
  styleUrls: ['./google-map.component.css']
})
export class GoogleMapComponent implements OnInit {
  @Output() addDestination = new EventEmitter();
  lat: number;
  lng: number;
  isFirstTime = true;
  public addrKeys: string[];
  public addr: object;
  locationMarker: LocationMarker[] = [];
  @Input() heightMap: any;
  constructor(private zone: NgZone) {
    this.setCurrentLocation();
  }

  ngOnInit() {}

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
}
