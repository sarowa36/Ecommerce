class FileUploadValue {
    file: File | null=null;
    link: String = "";
    isUploaded:boolean=false;
    constructor(p?: FileUploadValue) {
        if (p)
            Object.assign(this, p);
    }
}
export default FileUploadValue;