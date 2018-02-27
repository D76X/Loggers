Structured Enhanced L1 Header Style
==
Structured Enhanced L2 Header Style
-
The structured enhanced header styles have only L1 and L2.

---

Thee --- or *** create a horizontal ruler which converts to an hr tag.

***

# ATX Header Style L1
## ATX Header Style L2
### ATX Header Style L3
#### ATX Header Style L4
##### ATX Header Style L5
###### ATX Header Style L6

ATX Header Style span 6 levels.

This is a paragraph, paragraphs are created by insertin one or more blank 
lines between text. Line breaks in the text are ignored in the HTML output.

This is the following paragraph which is separated from the previous one by 
one or multiple blanck lines as said. It is possible to force a line break
in the flow of the text by appending two spaces at the ned of the poaragraph 
where you want the break to be for example here.   
BREAK!

***

## Block Quotes

> Block quotes are converted to a blockquote tag. To end a blockquote it is 
> sufficiant to leave one or more blank lines following its content as it is
> the case for paragraphs. 
> > Block quotes can also be nested like this.
> > In a block quote You can use any markdown such as _italic_  or **bold**.

***

## Text

The style applied to text can be controlled with tags such as _italic_ which 
maps to the **\<em>** tag or **bold** which maps to the **\<strong>** tag.
To escape characters such as < prepend them with \ which is useful to embed 
html/xml tags in the flow of the document. Howver, something like 
<strong>this</strong> is valid that is valid html tags can are also valid 
markdown.  

## Lists

### Unordered lists
- One 
- Two
* Three
* Four

These map to **\<ul>** and **\<li>** tags.

### Ordered lists
1. First
2. Second
3. Third

These map to **\<ol>** and **\<li>** tags.

### Links

Links and Images can both be written in two styles, either inline syntax or 
reference syntax.

- Inline syntax means that all the information about the link or image is written in one 
spot.

- Reference syntax means is in two parts. A piece that indicate the location in the document
where the asset must be shown. The other piece that reference the link or image to show.  

This is an inline link [BBC News](http://www.bbc.co.uk/news). It can be any valid URL pointing 
to either any local or remote resource. 

Links such \[CLICK HERE](http://someresource.html) are rendered to **\<a>CLICK HERE\<\a>** in 
HTML with its **'href'** arrtibute value set to the given URL. 

### Images

The markdown syntax for images is the same as for links but a ! is used in front of the square
brakets.

![Git workflow](/files/GitWokflow.png)
![Bomb](/04.png)
![NTT](/Assets/Icons/ntt_logo.png)