export function getEnumKeys<
    T extends string,
    TEnumValue extends number,
>(enumVariable: { [key in T]: TEnumValue }) {
    return Object.keys(enumVariable).filter((v) => isNaN(Number(v))) as Array<T>;
} 

