# CSZeroMQ

If there weren't already enough options for using ZeroMQ in C#, you have one more now. This library provides an 
efficient, minimal abstraction on top of [libzmq](https://github.com/zeromq/libzmq) in a similar manner to 
[cppzmq](https://github.com/zeromq/libzmq). In addition, the underlying handles and native functions are exposed
to allow you to use other APIs I have yet to create proper bindings for.

## But why?

I wrote this binding because the existing bindings didn't support what I needed.
[NetMQ](https://github.com/zeromq/netmq) only supports TCP and PGM, and [clrzmq](https://github.com/zeromq/clrzmq) 
seems to be abandoned.

## License
This library is licensed under the LGPL, version 3.0 or later. See `LICENSE.md` for details. A copy of the GPL is also 
provided at `doc/gpl-3.0.md` for your convenience.