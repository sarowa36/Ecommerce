import { FileUploadValue } from "./";
class FileUploadValueArray extends Array<FileUploadValue>{
    GetFiles(): Array<File> {
        return this.filter(x => x.file != null).map(x => x.file);
    }
    AppendFilesToFormData(valueName: string, formData: FormData): void {
        this.GetFiles().forEach(x => formData.append(valueName, x))
    }
}
export default FileUploadValueArray;