'''
Uses the output of zmq_doc_process.json to generate socket option enum values.
'''

import sys
import json

OPT_TYPE = "type"

INT_TMAP = {
    "int": "typeof(int)",
    "uint64_t": "typeof(ulong)",
    "int64_t": "typeof(long)",
    "int on POSIX systems, SOCKET on Windows": "typeof(int), WindowsType = typeof(nuint)",
}

def to_pascal_case(s: str):
    return "".join(map(lambda s: s.replace("IVL", "INTERVAL").title(), s.split("_")[1:]))

file: dict[str, dict] = None
with open("out.json") as df:
    file = json.load(df)

types = set()

for key in file:
    pass
    if "int" in file[key]['type']:
        pass
        # print(f"    [SockOptType({INT_TMAP[file[key]['type']]})]\n    {to_pascal_case(key)} = {file[key]['value']},")
    elif "character string" in file[key]['type']:
        pass
        null_term = bool("NULL-terminated" in file[key]['type'])
        print(f"    [StringSockOptDescriptor({str(null_term).lower()})]")
        print(f"    {to_pascal_case(key)} = {file[key]['value']},")
    elif "binary data" in file[key]['type']:
        pass
        # print(f"    {to_pascal_case(key)} = {file[key]['value']},")
    else:
        types.add(file[key]['type'])

print(types)