namespace SetLabels
{
    enum DrawingViewType
    {
        ЛИСТ = 1,
        РАЗРЕЗ = 2,
        МЕСТНЫЙ_ВИД = 3,
        ПРОЕКЦИОННЫЙ_ВИД = 4,
        ВСПОМОГАТЕЛЬНЫЙ_ВИД = 5,
        СТАНДАРТНЫЙ_ВИД = 6

        /****************  FROM SOLIDWORKS API **********************
         * 
        swDrawingAlternatePositionView	10 = Alternate position view
        swDrawingAuxiliaryView	5 = Auxiliary view
        swDrawingDetachedView	9 = Detached view
        swDrawingDetailView	3 = Detail view
        swDrawingNamedView	7 = Named view
        swDrawingProjectedView	4 = Projected (unfolded) view
        swDrawingRelativeView	8 = Relative view to the model
        swDrawingSectionView	2 = Section view
        swDrawingSheet	1 = Drawing sheet
        swDrawingStandardView	6 = Standard view 
         */
    }
}
