import { ElementRef } from '@angular/core';

export class ImageFormStructure {
    options = {
        fileSize: 2048, // in Bytes (by default 2048 Bytes = 2 MB)
        minWidth: 0, // minimum width of image that can be uploaded (by default 0, signifies any width)
        maxWidth: 0,  // maximum width of image that can be uploaded (by default 0, signifies any width)
        minHeight: 0,  // minimum height of image that can be uploaded (by default 0, signifies any height)
        maxHeight: 0,  // maximum height of image that can be uploaded (by default 0, signifies any height)
        fileType: ['image/gif', 'image/jpeg', 'image/png'], // mime type of files accepted
        height: 400, // height of cropper
        quality: 0.1, // quaity of image after compression
        crop: [  // array of objects for mulitple image crop instances (by default null, signifies no cropping)
          {
            ratio: 1, // ratio in which image needed to be cropped (by default null, signifies ratio to be free of any restrictions)
            minWidth: 0, // minimum width of image to be exported (by default 0, signifies any width)
            maxWidth: 0,  // maximum width of image to be exported (by default 0, signifies any width)
            minHeight: 0,  // minimum height of image to be exported (by default 0, signifies any height)
            maxHeight: 0,  // maximum height of image to be exported (by default 0, signifies any height)
            width: 0,  // width of image to be exported (by default 0, signifies any width)
            height: 0,  // height of image to be exported (by default 0, signifies any height)
          }
        ]
    };

    fileToUpload: ElementRef;
}
