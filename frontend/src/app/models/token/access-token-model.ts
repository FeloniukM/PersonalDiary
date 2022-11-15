import { AccessToken } from "./access-token";

export interface AccessTokenModel {
    accessToken: AccessToken;
    refreshToken: string;
}
