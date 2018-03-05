# Resources

[The GitHub Markdown Tutorial](https://www.markdowntutorial.com/)  
[Writing Content With Markdown - Pluralsight course](https://app.pluralsight.com/library/courses/writing-content-with-markdown/table-of-contents)  
[Markdown Editor for Visual Studio](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor)  

***

# How to force a break

In order to force a break in Markdown type two white spaces at the end of the line like 
below. This is different from ordered and unordered lists.  
  
FirstLine  
Second line  

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

## Links

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

## Images (and styles)

Adding images to markdown can be tricky. The markdown syntax for images is the same as for links but a **!** is used in front of the square brakets. The following example also shows how to add local styles 
to the markdown. For example here the image is floated to the left anf the text floats to its right. 

```
![Image](http://octodex.github.com/images/octdrey-catburn.jpg){: style="width:30%; float: left"}
```

The code above works well with in [Markdown Editor for Visual Studio](https://marketplace.visualstudio.com/items?) as it understands the syntax __`{: style="width:30%; float: left"}`__ but does not in VSCODE which appears unable to undertand local styles. In order to make it work the simplest course is to just embed the image by using teh html tag.

</br>

<img src="http://octodex.github.com/images/octdrey-catburn.jpg" width="200" height="200" />

***

In VSCODE you the Strict default setting is applied and might cause remote images to not display in the preview. You may change the settings either for the workspace or at user level.   

***

Styles may be defined locally in the markdown file or may be defined in a stylesheet to which the 
markdown referes. This is dependent upon the specific implementation of the markdoen plugin.  

With the [Markdown Editor for Visual Studio](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor)
you just need to have a file named **md-styles.css** in the same folder as the markdown files to 
style with it.

</br>

### Local image

Embedding local images is just slightly trickier as the syntax for the URL may be difficult to guess
and depends on the specific markdown pluging. With the  [Markdown Editor for Visual Studio](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.MarkdownEditor)
there is a trick to get it right. Just drag and drop the image from the folder onto the markdown 
document and the plugin generates the right syntaxt for you, then you can make changes to it as desired.  

![Bomb](Bomb.png)

***

## Tables

These are ways to display contents in a tabular form. Unfortunately, this works in VSCODE but not in VS2017 with the [Markdown Editor for Visual Studio](https://marketplace.visualstudio.com/items?).

First Header  | Second Header
------------- | -------------
Content Cell  | Content Cell
Content Cell  | Content Cell

</br>

| Command | Description |
| --- | --- |
| git status | List all new or modified files |
| git diff | Show file differences that haven't been staged |


***

