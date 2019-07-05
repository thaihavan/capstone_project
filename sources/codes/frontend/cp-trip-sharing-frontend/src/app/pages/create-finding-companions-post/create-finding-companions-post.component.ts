import { Component, OnInit, ViewChild } from '@angular/core';
import * as DecoupledEditor from '@ckeditor/ckeditor5-build-decoupled-document';
import { UploadImageComponent } from 'src/app/shared/components/upload-image/upload-image.component';
import { UploadAdapter } from 'src/app/model/UploadAdapter';
import { UploadImageService } from 'src/app/core/services/upload-image-service/upload-image.service';
@Component({
  selector: 'app-create-finding-companions-post',
  templateUrl: './create-finding-companions-post.component.html',
  styleUrls: ['./create-finding-companions-post.component.css']
})
export class CreateFindingCompanionsPostComponent implements OnInit {
  @ViewChild('uploadImage') uploadImage: UploadImageComponent;
  @ViewChild('myEditor') myEditor;
  imgUrl;
  isHasImg = false;
  isPublic = true;
  title: string;
  content = '<p>Hello world!</p>';
  public Editor = DecoupledEditor;

  constructor(private imageService: UploadImageService) { }
  ngOnInit() {
  }
  public onReady(editor) {
    editor.ui
      .getEditableElement()
      .parentElement.insertBefore(
        editor.ui.view.toolbar.element,
        editor.ui.getEditableElement()
      );
    editor.plugins.get('FileRepository').createUploadAdapter = loader => {
      // tslint:disable-next-line:no-string-literal
      console.log(loader['file']);
      return new UploadAdapter(loader, this.imageService);
    };
  }

  fileClick() {
    this.uploadImage.file.nativeElement.click();
  }
  ImageCropted(image) {
    this.imgUrl = image;
    this.isHasImg = true;
  }
  removeImage() {
    this.imgUrl = '';
    this.isHasImg = false;
  }

}
