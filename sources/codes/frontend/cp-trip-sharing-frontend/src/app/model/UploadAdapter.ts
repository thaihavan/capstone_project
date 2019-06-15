import { HttpParams, HttpClient, HttpHeaders } from '@angular/common/http';
import { observable } from 'rxjs';
class ImagePost {
image: any;
type: string;
}
export class UploadAdapter {
    constructor(
      private loader,
      public url: string,
      private http: HttpClient
      ) {
    this.ImagePost = new ImagePost();
    }
    ImagePost: ImagePost;
// the uploadFile method use to upload image to your server
  uploadFile(file, url?: string, user?: string) {
    // let name = '';
    url = 'api/posts';
    // const formData: FormData = new FormData();
    // const headers = new Headers();
    // name = file.name;
    // formData.append('attachment', file, name);
    // const dotIndex = name.lastIndexOf('.');
    // const fileName  = dotIndex > 0 ? name.substring(0, dotIndex) : name;
    // formData.append('name', fileName);
    // formData.append('source', user);
    this.ImagePost.type = file.type;
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      // me.modelvalue = reader.result;
      this.ImagePost.image = reader.result;
    //   console.log(reader.result);
    };
    reader.onerror = (error) => {
      console.log('Error: ', error);
    };

    // headers.append('Content-Type', 'multipart/form-data');
    // headers.append('Accept', 'application/json');
    // console.log('formData', formData);
    const params = new HttpParams();
    const options = {
        params,
        reportProgress: true,
        headers: new HttpHeaders({
            'Content-Type': 'application/json'
          })
    };
// http post return an observer
// so I need to convert to Promise
    return this.http.post<ImagePost>(url, this.ImagePost, options);
  }
// implement the upload
  upload() {
      const upload = new Promise((resolve, reject) => {
        this.loader.file.then(
            (data) => {
                this.uploadFile(data, this.url, 'test')
                .subscribe(
                    (result) => {
// resolve data formate must like this
// if **default** is missing, you will get an error
                        console.log(result.image);
                        resolve({ default: result.image });
                    },
                    (error) => {
                        reject(data.msg);
                    }
                );
            }
        );
      });
      return upload;
  }
  abort() {
      console.log('abort');
  }
}
