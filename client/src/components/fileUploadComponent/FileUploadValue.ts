class FileUploadValue {
    file:File
    base64Value:""
    constructor(p?: FileUploadValue) {
        if (p)
            Object.assign(this, p);
    }
}
export default FileUploadValue;