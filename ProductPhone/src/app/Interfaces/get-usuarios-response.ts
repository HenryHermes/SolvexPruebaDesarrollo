import { Usuarios } from "./usuarios";

export interface GetUsuariosResponse {
    message : string,
    result : Usuarios[],
    success : boolean
}
