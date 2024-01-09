class AddressItemValue {
    id: number | null = null;
    name: string = "";
    cityId: number | null = null;
    districtId: number | null = null;
    zip: string = "";
    detail: string = "";
    constructor(p: AddressItemValue | null) {
        if (p)
            Object.assign(this, p)
    }
}
export { AddressItemValue };