<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                xmlns:wix="http://schemas.microsoft.com/wix/2006/wi"
                exclude-result-prefixes="msxsl"
>
  <xsl:output method="xml"
              indent="yes"/>  
  
  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()"/>
    </xsl:copy>
  </xsl:template>
  
  <xsl:template match="wix:Directory" >
    <!-- Apply identity transform on child elements of Directory-->
    <xsl:apply-templates select="*"/>
    <!--<xsl:apply-templates select="Component"/>-->
    <xsl:copy-of select="*"/>
  </xsl:template>  
  
</xsl:stylesheet>
