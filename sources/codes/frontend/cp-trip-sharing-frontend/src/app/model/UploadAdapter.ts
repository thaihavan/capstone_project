import { HttpParams, HttpClient, HttpHeaders } from '@angular/common/http';
import { observable } from 'rxjs';
class ImagePost {
  image: any;
  type: string;
}
export class UploadAdapter {
  url = 'https://localhost:44352/api/postservice/uploadfile/uploadimage';
  constructor(private loader, private http: HttpClient) {
    this.ImagePost = new ImagePost();
  }
  ImagePost: ImagePost;
  // implement the upload
  upload() {
    const upload = new Promise((resolve, reject) => { this.loader.file
            .then( file  => {
                  const myReader = new FileReader();
                  myReader.onloadend = (e) => {
                     const image = myReader.result.toString().split(',');
                     this.ImagePost.image = image[1];
                     this.ImagePost.type = file.type;
                     this.http.post<ImagePost>(this.url, this.ImagePost).subscribe(
                      url => {
                        console.log('my url', url.image);
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
