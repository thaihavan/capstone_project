import { Component, OnInit, NgZone } from '@angular/core';
import { LocationMarker } from 'src/app/model/LocationMarker';

@Component({
  selector: 'app-google-map',
  templateUrl: './google-map.component.html',
  styleUrls: ['./google-map.component.css']
})
export class GoogleMapComponent implements OnInit {
  title = 'My first AGM project';
  lat: number;
  lng: number;
  public addrKeys: string[];
  public addr: object;
  locationMarker: LocationMarker [] = [];
  constructor(private zone: NgZone) {
    this.setCurrentLocation();
   }

  ngOnInit() {
  }
  setAddress(addrObj) {
    this.zone.run(() => {
      this.addr = addrObj;
      this.addrKeys = Object.keys(addrObj);
      const location: LocationMarker = {
        longtitude: addrObj.lng,
        lattitude: addrObj.lat,
        country: addrObj.country,
        locality: addrObj.locality,
        icon: ''
      };
      this.locationMarker.push(location);
      this.lat = addrObj.lat;
      this.lng = addrObj.lng;
    });
  }
  private setCurrentLocation() {
    if ('geolocation' in navigator) {
      navigator.geolocation.getCurrentPosition((position) => {
        const location: LocationMarker = {
          longtitude: position.coords.longitude,
          lattitude: position.coords.latitude,
          country: 'VN',
          locality: '',
          icon: ''
        };
        this.locationMarker.push(location);
        this.lat = position.coords.latitude;
        this.lng = position.coords.longitude;
      });
    }
  }
}
