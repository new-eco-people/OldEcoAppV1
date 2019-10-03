import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-view-pictures',
  templateUrl: './view-pictures.component.html',
  styleUrls: ['./view-pictures.component.css']
})
export class ViewPicturesComponent implements OnInit {

  @Input() photos: any[];

  photoUrl = '';
  position = 0;

  constructor() { }

  ngOnInit() {
    this.setInitialPhoto();
  }

  setInitialPhoto() {
    this.photoUrl = this.photos[0].url as string;
  }

  nextPicture(e) {
    e.preventDefault();
    const position = this.position + 1;
    this.position = position >= this.photos.length ? 0 : position;
    this.photoUrl = this.photos[this.position].url;
    //console.log(position);
  }

  prevPicture(e) {
    e.preventDefault();
    const position = this.position - 1;
    this.position = position < 0 ? this.photos.length - 1 : position;
    this.photoUrl = this.photos[this.position].url;
    //console.log(position);
  }

}
