Userifccs.Curses
===========================================
.. .rst to .html: rst2html5 foo.rst > foo.html
..                pandoc -s -f rst -t html5 -o foo.html foo.rst

Curses sub-package for CSharp User interface example project.

Installation
------------
source code tarball download:
    
        # [aria2c --check-certificate=false | wget --no-check-certificate | curl -kOL]
        
        FETCHCMD='aria2c --check-certificate=false'
        
        $FETCHCMD https://bitbucket.org/thebridge0491/userifccs/[get | archive]/master.zip

version control repository clone:
        
        git clone https://bitbucket.org/thebridge0491/userifccs.git

build example with make:
[sh] ./configure.sh [--prefix=$PREFIX] [--help]

make build [test]

make nugetadd [nugetinstall]

build example with msbuild:
[env LD_LIBRARY_PATH=$PREFIX/lib] msbuild /t:build [/t:test]

msbuild /t:nugetpack,nugetadd [/t:nugetinstall]

Usage
-----
        // PKG_CONFIG='pkg-config --with-path=$PREFIX/lib/pkgconfig'
        
        // $PKG_CONFIG --cflags --libs <ffi-lib>

        using Userifccs.Curses;
        
        ...
        
        var model1 = new Curses.HelloModel("greet.txt");
        
        var view1 = new Curses.HelloFormView();
        
        model1.AttachObserver(view1);
        
        model1.NotifyObservers("To be set -- HELP.");
        
        Console.Write("view1.Data: {0}\n", view1.Data);

Author/Copyright
----------------
Copyright (c) 2013 by thebridge0491 <thebridge0491-codelab@yahoo.com>

License
-------
Licensed under the Apache-2.0 License. See LICENSE for details.
