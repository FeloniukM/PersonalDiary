import { ImageInfoModel } from "../image/image-info-model";

export interface RecordInfoModel {
    id: string;
    createdAt: Date;
    updateAt: Date;
    title: string;
    text: string;
    image?: ImageInfoModel
}
