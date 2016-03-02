Clever
=====

[![Build status](https://ci.appveyor.com/api/projects/status/mss8wwf3k0rdqxev/branch/master?svg=true)](https://ci.appveyor.com/project/beattyml1/clever/branch/master)

Clever is an opinionated, convention first, web framework build on top of .NET MVC and (optionally) React.JS. It's philosophy is convention over configuration but does so by layering sensible defaults on top of each other to give you great conventions that break down gracefully as you need to configure. It is designed so that you can use some parts but not all. So you can use the web N-Tier Web stuff without the React stuff or the data access modules.

When you use it to it's fullest potential most boilplate code just goes away and behaviors are sensibly interpreted from lower layers unless you need to override. And if you let it configure your stuff it will even handle stuff like routes, dependency injection setup, and even server side rendering of react components with data refresh from RESTful resource.
