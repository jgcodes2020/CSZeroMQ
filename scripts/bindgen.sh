#!/usr/bin/env bash
SCRIPT_DIR=$( cd -- "$( realpath -P -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )
# shellcheck disable=SC2164
cd "$SCRIPT_DIR/.."
ClangSharpPInvokeGenerator \
  -f native/zmq.h -n "CSZeroMQ.Native" -m "ZMQ" -o CSZeroMQ/Native/ZMQ.gen.cs \
  -I /usr/lib/clang/15.0.7/include -x c -l libzmq -r char=byte
