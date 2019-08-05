import { ImageUpload } from './ImageUpload';
import { UploadImageService } from '../core/services/upload-image-service/upload-image.service';

export class UploadAdapter {
  constructor(private loader, private uploadImageService: UploadImageService) {
    this.ImagePost = new ImageUpload();
  }
  ImagePost: ImageUpload;
  // implement the upload
  upload() {
    const upload = new Promise((resolve, reject) => { this.loader.file
            .then( file  => {
                  const myReader = new FileReader();
                  myReader.onloadend = (e) => {
                     const image = myReader.result.toString().split(',');
                     this.ImagePost.image = image[1];
                     this.ImagePost.type = file.type;
                     this.uploadImageService.uploadImage(this.ImagePost).subscribe(
                      url => {
                        resolve({ default: url.image});
                       }
                     );
                    };
                  myReader.readAsDataURL(file);
            } );
          });
    return upload;
  }
  abort() {
    console.log('abort');
  }
}
