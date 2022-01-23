import {Component, EventEmitter, Output} from '@angular/core';
import {HtmlEditorService, ImageService, LinkService, ToolbarService} from "@syncfusion/ej2-angular-richtexteditor";

@Component({
  selector: 'app-rich-text-editor',
  templateUrl: './rich-text-editor.component.html',
  styleUrls: ['./rich-text-editor.component.css'],
  providers: [ToolbarService, LinkService, ImageService, HtmlEditorService]
})
export class RichTextEditorComponent {

  description: string = '';
  @Output() private descriptionSend = new EventEmitter();

  constructor() {
  }

  onClick() {
    console.log(this.description)
    this.descriptionSend.emit(this.description);
    return false;
  }
}
