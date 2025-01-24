import { ProductoCategoria } from "./producto-categoria";

export interface GetResponseProductoCategoria {
    message : string,
    result : ProductoCategoria[],
    success : boolean
}
