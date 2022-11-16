import { AccessTokenModel } from "../token/access-token-model";
import { UserModel } from "../user/user-model";

export interface AuthUser {
    user: UserModel;
    token: AccessTokenModel;
}
