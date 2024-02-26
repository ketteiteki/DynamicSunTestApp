

export class UploadWeatherArchiveRequest {
    files: File[];

    constructor(files: File[]) {
        this.files = files;
    }
}