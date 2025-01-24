import { Productos } from "./productos";

export interface GetProductResponse {
    message : string,
    result : Productos[],
    success : boolean
}
