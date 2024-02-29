import { FileUploadValue } from "./";
class FileUploadValueArray extends Array<FileUploadValue>{
    GetFiles(): Array<File> {
        return this.filter(x => x.file != null).map(x => x.file);
    }
    AppendFilesToFormData(valueName: string, formData: FormData): void {
        this.forEach((element,index) => {
            if(element.isUploaded)
            formData.append(valueName+`[${index}].link`,element.link);
            else
            formData.append(valueName+`[${index}].file`,element.file);
        });
    }
}
export {FileUploadValueArray};