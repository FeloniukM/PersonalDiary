import { AccessTokenModel } from "../token/access-token-model";
import { User } from "../user";

export interface AuthUser {
    user: User;
    token: AccessTokenModel;
}
