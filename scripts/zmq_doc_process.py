'''
Custom script that extracts socket option info from the doc files.
'''

import sys
import json

KEYTBL = {
    "Option value type": "type",
    "Option value size": "size",
    "Option value unit": "unit",
    "Default value": "default",
    "Applicable socket types": "sock_types"
}

state = 0
optname = None
accum = {}
valtbl: dict[str, int] = {}

with open("opt_values.json") as ovf:
    valtbl = json.load(ovf)

with open("doc.txt") as docf:
    for line in docf:
        line = line[0:-1]
        match state: 
            case 0:
                if len(line) == 0:
                    continue
                
                pos = line.find(':')
                optname = line[0:pos]
                accum[optname] = {
                    "value": valtbl[optname]
                }
                state = 1
            case 1:
                if line != "[horizontal]":
                    continue
                state = 2
            case 2:
                if len(line) == 0:
                    state = 0
                    continue
                split = line.split(":: ")
                accum[optname][KEYTBL[split[0]]] = split[1]

with open("out.json", "w") as outf:
    json.dump(accum, outf, indent=2);